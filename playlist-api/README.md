## Triggers
Aqui gostaria de apresentar as triggers que foram usadas durante o processo de banco de dados, fiquei feliz em conseguir realizar todos os gatilhos necessários, gatilhos quais se acionam através dos métodos Post, Put e Delete;

- Trigger para atualizar (update) a playlist que esta inserida na música, atualizando também a duração de tempo da playlist em que estava e passando o tempo da música para a nova playlist:

      create trigger updateMusica
      on musica
      instead of update
      as
      begin

        declare @id int, @idPlaylist int, @duracaoemsegundos int 
     
        select @id = id, @duracaoemsegundos = duracaoemsegundos from inserted
	
        select @idPlaylist = idplaylist from musica where id = @id;

        update playlist set duracao -= @duracaoemsegundos where playlist.id = @idPlaylist;
     
        select @idPlaylist = idplaylist from inserted
     
        update musica set idplaylist = @idPlaylist where id = @id;
     
        update playlist set duracao += @duracaoemsegundos where playlist.id = @idPlaylist;

      end
      go

- Trigger para aumentar o tempo de uma playlist assim que receber uma nova música:

      create trigger trg_DuracaoDaPlaylist
      on musica
      after insert 
      as 
      begin 

        declare @duracao int, @idplaylist int

        select @duracao = duracaoemsegundos, @idplaylist = idPlaylist from inserted

        update playlist set duracao += @duracao where playlist.id = @idplaylist

      end
      go

- Trigger para diminuir o tempo de uma playlist assim que ela for deletada uma música for deletada:

      create trigger trg_DeleteMusicaDiminuiDuracaoPlaylist
      on musica
      instead of delete 
      as 
      begin

        declare @duracao int, @idplaylist int, @id int

        select @duracao = duracaoemsegundos, @idplaylist = idPlaylist, @id = id  from deleted

	      update playlist set duracao -= @duracao where playlist.id = @idplaylist;

	      delete from musica where id = @id;

      end
      go

  - Trigger usado para ser possível deletar uma playlist que possui músicas dentro, foi passado id de playlist para nulo pois o playlist.id é uma primary key, assim, sendo possível relacionar as duas tabelas e conseguirmos exibir as músicas que estão dentro de uma única playlist, como é realizado em um dos métodos get de musica:

        create trigger deletePlaylist
        on playlist 
        instead of delete
        as 
        begin 

          declare @id int
    
      	  select @id = id from deleted

          update musica set idPlaylist = null where idPlaylist = @id

          delete from playlist where id = @id

        end
        go

